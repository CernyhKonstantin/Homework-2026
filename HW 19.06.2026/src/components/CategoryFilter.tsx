interface CategoryFilterProps {
    category: string;
    setCategory: (value: string) => void;
}

function CategoryFilter({
    category,
    setCategory
}: CategoryFilterProps) {
    return (
        <select
            value={category}
            onChange={(e) => setCategory(e.target.value)}
        >
            <option value="All">All Categories</option>
            <option value="Audio">Audio</option>
            <option value="Gaming">Gaming</option>
            <option value="Accessories">Accessories</option>
            <option value="Displays">Displays</option>
            <option value="Storage">Storage</option>
            <option value="Wearables">Wearables</option>
        </select>
    );
}

export default CategoryFilter;