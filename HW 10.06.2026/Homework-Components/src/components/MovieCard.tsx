import interstellar from "../assets/interstellar.jpg";

type MovieProps = {
    title: string;
    director: string;
    year: number;
    studio: string;
};

function MovieCard(props: MovieProps) {
    return (
        <div className="card">
            <h2>{props.title}</h2>

            <img src={interstellar} width="250" />

            <p>Director: {props.director}</p>
            <p>Year: {props.year}</p>
            <p>Studio: {props.studio}</p>
        </div>
    );
}

export default MovieCard;