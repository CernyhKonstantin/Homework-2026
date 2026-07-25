import React from "react";

export default function ProductList({ products }) {

    return (
        <div>

            <h2>Products</h2>

            {
                products.map(product => (

                    <div key={product.id}>

                        <h3>{product.name}</h3>

                        <p>{product.description}</p>

                        <b>{product.price} €</b>

                    </div>

                ))
            }

        </div>
    );

}