import React, { Component } from "react";
import interstellar from "../assets/interstellar.jpg";

class MovieCardClass extends Component<any> {
    render() {
        return (
            <div className="card">
                <h2>{this.props.title}</h2>

                <img src={interstellar} width="250" />

                <p>Director: {this.props.director}</p>
                <p>Year: {this.props.year}</p>
                <p>Studio: {this.props.studio}</p>
            </div>
        );
    }
}

export default MovieCardClass;