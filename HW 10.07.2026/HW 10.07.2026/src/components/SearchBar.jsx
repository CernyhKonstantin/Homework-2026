import { useState } from "react";

export default function SearchBar({ onSearch }) {

    const [text, setText] = useState("");

    return (

        <div>

            <input
                value={text}
                onChange={(e) => setText(e.target.value)}
                placeholder="Search category"
            />

            <button onClick={() => onSearch(text)}>
                Search
            </button>

        </div>

    );

}