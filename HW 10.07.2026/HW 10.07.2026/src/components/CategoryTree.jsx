import { useState } from "react";
import api from "../services/api";

export default function CategoryTree({ categories, onProducts }) {

    const [children, setChildren] = useState({});

    async function openCategory(category) {

        const response = await api.get(`/Category/${category.id}/children`);

        if (response.data.length === 0) {

            const products = await api.get(`/Product/category/${category.id}`);

            onProducts(products.data);

        }
        else {

            setChildren({

                ...children,

                [category.id]: response.data

            });

        }

    }

    return (

        <div>

            {
                categories.map(category => (

                    <div key={category.id}>

                        <button onClick={() => openCategory(category)}>

                            {category.name}

                        </button>

                        {

                            children[category.id] &&

                            <CategoryTree

                                categories={children[category.id]}

                                onProducts={onProducts}

                            />

                        }

                    </div>

                ))
            }

        </div>

    );

}