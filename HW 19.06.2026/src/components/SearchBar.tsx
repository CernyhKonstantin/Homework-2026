interface SearchBarProps {
    search: string;
    setSearch: (value: string) => void;
}

function SearchBar({ search, setSearch }: SearchBarProps) {
    return (
        <input
            type="text"
            placeholder="Search products..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
        />
    );
}

export default SearchBar;