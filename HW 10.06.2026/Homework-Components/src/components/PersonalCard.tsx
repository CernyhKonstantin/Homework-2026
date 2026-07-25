import profile from "../assets/profile.jpg";

type PersonalProps = {
    name: string;
    phone: string;
    email: string;
    city: string;
    experience: string;
    skills: string[];
};

function PersonalCard(props: PersonalProps) {
    return (
        <div className="card">
            <h2>{props.name}</h2>

            <img src={profile} width="150" />

            <p>Phone: {props.phone}</p>
            <p>Email: {props.email}</p>
            <p>City: {props.city}</p>
            <p>Experience: {props.experience}</p>

            <h3>Skills</h3>
            <ul>
                {props.skills.map((s, i) => (
                    <li key={i}>{s}</li>
                ))}
            </ul>
        </div>
    );
}

export default PersonalCard;