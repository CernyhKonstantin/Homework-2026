import marienplatz from "../assets/marienplatz.jpg";
import nymphenburg from "../assets/nymphenburg.jpg";
import englischergarten from "../assets/englischergarten.jpg";

function CityInfo() {
    const attractions = [
        marienplatz,
        nymphenburg,
        englischergarten
    ];

    return (
        <div>
            <h2>Munich</h2>

            <p><strong>Country:</strong> Germany</p>

            <p><strong>Founded:</strong> 1158</p>

            <h3>Famous Attractions</h3>

            {attractions.map((image, index) => (
                <img
                    key={index}
                    src={image}
                    alt="Munich attraction"
                    width="300"
                />
            ))}
        </div>
    );
}

export default CityInfo;