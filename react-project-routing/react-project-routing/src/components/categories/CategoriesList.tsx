import { useEffect, useState } from "react";
import { useParams } from "react-router";

import type { CategoryType } from "@/types/CategoryType";
import type { ProductType } from "@/types/ProductType";
import Category from "@/components/categories/Category";
import Product from "@/components/product/Product";
import Categories from "@/utils/Category";
import Products from "@/utils/Product";

interface CategoriesListProps {
    itemsPerPage?: number;
}

const CategoriesList = ({ itemsPerPage = 3 }: CategoriesListProps) => {
    const [categories, setCategories] = useState<CategoryType[]>([]);
    const [products, setProducts] = useState<ProductType[]>([]);
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);

    const { id } = useParams();

    useEffect(() => {
        // Reset to page 1 whenever the category ID in the URL changes
        setCurrentPage(1);

        const loadCategories = async () => {
            setLoading(true);

            try {
                const data = await Categories.GetAllCategories();

                if (data) {
                    setProducts([]);
                    setCategories(
                        data.filter(category => category.parentId === null)
                    );
                }
            } finally {
                setLoading(false);
            }
        };

        const loadSubCategories = async (categoryId: number) => {
            setLoading(true);

            try {
                const data = await Categories.GetSubCategoryById(categoryId);

                if (data && data.length > 0) {
                    setProducts([]);
                    setCategories(data);
                } else {
                    const fetchedProducts = await Products.GetProductsByCategoryId(categoryId);

                    setCategories([]);
                    setProducts(fetchedProducts ?? []);
                }
            } finally {
                setLoading(false);
            }
        };

        if (id) {
            loadSubCategories(Number(id));
        } else {
            loadCategories();
        }

    }, [id]);

    if (loading) {
        return <div className="text-center py-8 text-gray-500">Loading...</div>;
    }

    // Determine total items depending on whether we display categories or products
    const activeItems = categories.length > 0 ? categories : products;
    const totalPages = Math.ceil(activeItems.length / itemsPerPage);

    // Slice items for current page
    const startIndex = (currentPage - 1) * itemsPerPage;
    const currentCategories = categories.slice(startIndex, startIndex + itemsPerPage);
    const currentProducts = products.slice(startIndex, startIndex + itemsPerPage);

    const handlePrevPage = () => {
        setCurrentPage((prev) => Math.max(prev - 1, 1));
    };

    const handleNextPage = () => {
        setCurrentPage((prev) => Math.min(prev + 1, totalPages));
    };

    const handlePageClick = (page: number) => {
        setCurrentPage(page);
    };

    if (categories.length === 0 && products.length === 0) {
        return <p className="text-center text-gray-500 my-4">No items found</p>;
    }

    return (
        <div className="flex flex-col items-center gap-6 w-full">
            {/* Grid display for current page items */}
            <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 w-full">
                {currentCategories.map(category => (
                    <Category
                        key={category.id}
                        category={category}
                    />
                ))}

                {currentProducts.map(product => (
                    <Product
                        key={product.id}
                        product={product}
                    />
                ))}
            </div>

            {/* Pagination Controls */}
            {totalPages > 1 && (
                <div className="flex items-center gap-2 mt-6">
                    <button
                        onClick={handlePrevPage}
                        disabled={currentPage === 1}
                        className="px-3 py-1 border rounded cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed hover:bg-gray-100 transition-colors"
                    >
                        Previous
                    </button>

                    {Array.from({ length: totalPages }, (_, index) => {
                        const pageNum = index + 1;
                        return (
                            <button
                                key={pageNum}
                                onClick={() => handlePageClick(pageNum)}
                                className={`px-3 py-1 border rounded cursor-pointer transition-colors ${currentPage === pageNum
                                        ? 'bg-blue-600 text-white border-blue-600'
                                        : 'hover:bg-gray-100'
                                    }`}
                            >
                                {pageNum}
                            </button>
                        );
                    })}

                    <button
                        onClick={handleNextPage}
                        disabled={currentPage === totalPages}
                        className="px-3 py-1 border rounded cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed hover:bg-gray-100 transition-colors"
                    >
                        Next
                    </button>
                </div>
            )}
        </div>
    );
};

export default CategoriesList;