import "./App.css";

import CityInfo from "./components/CityInfo";
import CityInfoClass from "./components/CityInfoClass";
import BookInfo from "./components/BookInfo";

function App() {
    return (
        <div className="container">
            <h1>Homework 08.06.2026</h1>

            <CityInfo />

            <hr />

            <CityInfoClass />

            <hr />

            <BookInfo />
        </div>
    );
}

export default App;