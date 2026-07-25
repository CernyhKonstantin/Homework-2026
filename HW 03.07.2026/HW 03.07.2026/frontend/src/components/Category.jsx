import React, { useEffect, useState } from "react";

const API_URL = "https://localhost:7049/api/category";

export default function Category() {
    const [categories, setCategories] = useState([]);
    const [form, setForm] = useState({
        name: "",
        slug: "",
        parentId: ""
    });

    // GET all categories
    const loadCategories = async () => {
        try {
            const res = await fetch(API_URL);
            const data = await res.json();
            setCategories(data);
        } catch (err) {
            console.log("Error loading categories:", err);
        }
    };

    useEffect(() => {
        loadCategories();
    }, []);

    // input change
    const handleChange = (e) => {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    // POST create category
    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            name: form.name,
            slug: form.slug,
            parentId: form.parentId ? parseInt(form.parentId) : null
        };

        await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });

        setForm({ name: "", slug: "", parentId: "" });
        loadCategories();
    };

    return (
        <div>
            <h2>Categories</h2>

            {/* FORM */}
            <form onSubmit={handleSubmit}>
                <input
                    name="name"
                    placeholder="Name"
                    value={form.name}
                    onChange={handleChange}
                />

                <input
                    name="slug"
                    placeholder="Slug"
                    value={form.slug}
                    onChange={handleChange}
                />

                <input
                    name="parentId"
                    placeholder="Parent ID (optional)"
                    value={form.parentId}
                    onChange={handleChange}
                />

                <button type="submit">Add Category</button>
            </form>

            {/* LIST */}
            <ul>
                {categories.map((c) => (
                    <li key={c.id}>
                        {c.name} ({c.slug}) - ID: {c.id}
                    </li>
                ))}
            </ul>
        </div>
    );
}