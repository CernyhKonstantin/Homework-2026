from datetime import datetime, timezone
from django.http import HttpResponse
from django.shortcuts import redirect
from django.utils import timezone as django_timezone
AGE_30_DAYS=60*60*24*30

class RateLimitMiddleware:
    LIMIT=10
    WINDOW=60
    def __init__(self,get_response): self.get_response=get_response
    def __call__(self,request):
        now=datetime.now(timezone.utc).timestamp()
        stamps=[]
        for raw in request.COOKIES.get("request_times","").split(","):
            try:
                stamp=float(raw)
                if 0 <= now-stamp < self.WINDOW: stamps.append(stamp)
            except (TypeError,ValueError): pass
        if len(stamps)>=self.LIMIT:
            response=HttpResponse("Too many requests. Please wait and try again.",status=429)
            response["Retry-After"]=str(max(1,int(self.WINDOW-(now-min(stamps)))))
        else:
            stamps.append(now)
            response=self.get_response(request)
        response.set_cookie("request_times",",".join(map(str,stamps)),max_age=self.WINDOW,httponly=True,samesite="Lax")
        return response

class VisitMiddleware:
    def __init__(self,get_response): self.get_response=get_response
    def __call__(self,request):
        try: count=max(0,int(request.COOKIES.get("visit_count","0")))+1
        except (TypeError,ValueError): count=1
        visited_at=django_timezone.now().isoformat()
        response=self.get_response(request)
        response.set_cookie("visit_count",str(count),max_age=AGE_30_DAYS,httponly=True,samesite="Lax")
        response.set_cookie("last_visit",visited_at,max_age=AGE_30_DAYS,httponly=True,samesite="Lax")
        return response

class RulesMiddleware:
    EXEMPT=("/rules/","/admin/","/favicon.ico")
    def __init__(self,get_response): self.get_response=get_response
    def __call__(self,request):
        if not request.path.startswith(self.EXEMPT) and not request.COOKIES.get("accepted_rules"):
            return redirect("rules")
        return self.get_response(request)
