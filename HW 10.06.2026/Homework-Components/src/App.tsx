import "./App.css";

import MovieCard from "./components/MovieCard";
import PersonalCard from "./components/PersonalCard";
import MovieCardClass from "./components/MovieCardClass";
import PersonalCardClass from "./components/PersonalCardClass";

function App() {
    return (
        <div className="container">
            <h1>Homework Components & Props</h1>

            <MovieCard
                title="Interstellar"
                director="Christopher Nolan"
                year={2014}
                studio="Paramount Pictures"
            />

            <PersonalCard
                name="Max Mustermann"
                phone="+49 123 456789"
                email="max@example.com"
                city="Munich"
                experience="2 years warehouse worker"
                skills={["Teamwork", "JavaScript", "React"]}
            />

            <hr />

            <MovieCardClass
                title="Interstellar"
                director="Christopher Nolan"
                year={2014}
                studio="Paramount Pictures"
            />

            <PersonalCardClass
                name="Max Mustermann"
                phone="+49 123 456789"
                email="max@example.com"
                city="Munich"
                experience="2 years warehouse worker"
                skills={["Teamwork", "JavaScript", "React"]}
            />
        </div>
    );
}

export default App;