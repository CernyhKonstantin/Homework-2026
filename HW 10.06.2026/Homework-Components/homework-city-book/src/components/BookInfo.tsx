function BookInfo() {
    const reviews = [
        "A magical and unforgettable adventure.",
        "Perfect for readers of all ages.",
        "One of the most influential fantasy books ever written."
    ];

    return (
        <div>
            <h2>Harry Potter and the Philosopher's Stone</h2>

            <p><strong>Author:</strong> J. K. Rowling</p>

            <p><strong>Genre:</strong> Fantasy</p>

            <p><strong>Pages:</strong> 223</p>

            <h3>Reviews</h3>

            <ul>
                {reviews.map((review, index) => (
                    <li key={index}>{review}</li>
                ))}
            </ul>
        </div>
    );
}

export default BookInfo;