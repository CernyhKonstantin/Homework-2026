import { FaHeart } from "react-icons/fa";

interface FavoriteButtonProps {
    onClick: () => void;
}

function FavoriteButton({ onClick }: FavoriteButtonProps) {
    return (
        <button className="favorite-button" onClick={onClick}>
            <FaHeart />
        </button>
    );
}

export default FavoriteButton;