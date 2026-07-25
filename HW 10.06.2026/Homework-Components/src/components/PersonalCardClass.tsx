import React, { Component } from "react";
import profile from "../assets/profile.jpg";

class PersonalCardClass extends Component<any> {
    render() {
        return (
            <div className="card">
                <h2>{this.props.name}</h2>

                <img src={profile} width="150" />

                <p>Phone: {this.props.phone}</p>
                <p>Email: {this.props.email}</p>
                <p>City: {this.props.city}</p>
                <p>Experience: {this.props.experience}</p>

                <h3>Skills</h3>
                <ul>
                    {this.props.skills.map((s: string, i: number) => (
                        <li key={i}>{s}</li>
                    ))}
                </ul>
            </div>
        );
    }
}

export default PersonalCardClass;