from urllib.parse import quote

import requests
from django.conf import settings
from django.core.cache import cache

BASE_URL = "https://api.restcountries.com/countries/v5"
CACHE_SECONDS = 600


class RestCountriesError(Exception):
    """Raised when the external country API cannot provide usable data."""


def _request(endpoint="", params=None):
    api_key = getattr(settings, "REST_COUNTRIES_API_KEY", "").strip()
    if not api_key:
        raise RestCountriesError(
            "A REST Countries API key is required. Create a free key at "
            "https://restcountries.com/sign-up and set REST_COUNTRIES_API_KEY "
            "in your PowerShell terminal."
        )
    if api_key == "rc_live_demo":
        raise RestCountriesError(
            "The REST Countries demo key returns sample data only (for example, Canada). "
            "It does not provide the complete country list. Create your own API key at "
            "https://restcountries.com/sign-up and set REST_COUNTRIES_API_KEY."
        )

    url = f"{BASE_URL}/{endpoint.lstrip('/')}" if endpoint else BASE_URL
    headers = {"Authorization": f"Bearer {api_key}"}
    try:
        response = requests.get(url, headers=headers, params=params or {}, timeout=20)
        response.raise_for_status()
        payload = response.json()
    except requests.RequestException as exc:
        raise RestCountriesError(
            "The country API could not be reached. Check your internet connection and API key, then try again."
        ) from exc
    except ValueError as exc:
        raise RestCountriesError("The country API returned invalid JSON data.") from exc

    if isinstance(payload, dict) and payload.get("errors"):
        first_error = payload["errors"][0]
        message = first_error.get("message", "Unknown API error") if isinstance(first_error, dict) else str(first_error)
        raise RestCountriesError(f"REST Countries API error: {message}")
    return payload


def _data_section(payload):
    if isinstance(payload, dict):
        return payload.get("data", payload)
    return payload


def _objects(payload):
    data = _data_section(payload)
    if isinstance(data, dict):
        objects = data.get("objects", data.get("object", []))
        if isinstance(objects, dict):
            return [objects]
        return objects if isinstance(objects, list) else []
    return data if isinstance(data, list) else []


def _has_more(payload, page_size):
    data = _data_section(payload)
    meta = data.get("meta", {}) if isinstance(data, dict) else {}
    if isinstance(meta, dict) and "more" in meta:
        return bool(meta["more"])
    return page_size == 100


def country_name(country):
    names = country.get("names") or {}
    return names.get("common") or names.get("official") or "Unknown country"


def country_code(country):
    codes = country.get("codes") or {}
    return str(codes.get("alpha_3") or codes.get("alpha_2") or "")


def get_all_countries():
    cached = cache.get("all_rest_countries_v5")
    if cached is not None:
        return cached

    records = []
    offset = 0
    while True:
        payload = _request(params={"limit": 100, "offset": offset})
        page = _objects(payload)
        records.extend(page)
        if not page or not _has_more(payload, len(page)):
            break
        offset += len(page)

    records.sort(key=lambda item: country_name(item).casefold())
    cache.set("all_rest_countries_v5", records, CACHE_SECONDS)
    return records


def get_country_by_code(code):
    wanted = code.casefold()
    for item in get_all_countries():
        if country_code(item).casefold() == wanted:
            return item
    raise RestCountriesError("Country not found.")


def get_countries_by_region(region):
    payload = _request(f"region/{quote(region, safe='')}", {"limit": 100})
    return sorted(_objects(payload), key=lambda item: country_name(item).casefold())


def get_capital_information(capital):
    payload = _request(f"capitals/{quote(capital, safe='')}", {"limit": 100})
    return _objects(payload)


def language_values(country):
    raw = country.get("languages") or []
    if isinstance(raw, dict):
        return [str(value) for value in raw.values()]
    values = []
    for entry in raw:
        if isinstance(entry, dict):
            values.extend(
                str(entry[key])
                for key in ("name", "common", "english", "native")
                if entry.get(key)
            )
        elif entry:
            values.append(str(entry))
    return values


def get_languages():
    return sorted(
        {value for country in get_all_countries() for value in language_values(country)},
        key=str.casefold,
    )


def get_countries_by_language(language):
    target = language.casefold()
    return [
        country for country in get_all_countries()
        if any(target == value.casefold() or target in value.casefold() for value in language_values(country))
    ]


def display_data(country):
    names = country.get("names") or {}
    codes = country.get("codes") or {}
    flag = country.get("flag") or {}
    capitals = country.get("capitals") or country.get("capital") or []
    if isinstance(capitals, str):
        capitals = [capitals]
    if isinstance(capitals, dict):
        capitals = [capitals.get("name") or capitals.get("common") or ""]
    area = country.get("area")
    if isinstance(area, dict):
        area = area.get("kilometers")
    return {
        "name": names.get("common") or names.get("official") or "Unknown",
        "official_name": names.get("official") or "Not available",
        "code": codes.get("alpha_3") or codes.get("alpha_2") or "",
        "flag": flag.get("emoji") or "🌍",
        "capital": capitals[0] if capitals else None,
        "region": country.get("region") or "Not available",
        "subregion": country.get("subregion") or "Not available",
        "population": country.get("population"),
        "area": area,
        "languages": language_values(country),
        "timezones": country.get("timezones") or [],
        "continents": country.get("continents") or [],
    }
