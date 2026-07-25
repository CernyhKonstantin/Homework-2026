import { createBrowserRouter } from "react-router";

import Layout from "@/components/layout/Layout.tsx";

import Home from "@/components/pages/Home.tsx";
import About from "@/components/pages/About.tsx";
import CategoriesList from "@/components/categories/CategoriesList.tsx";
import CategoryDetail from "@/components/categories/CategoryDetail.tsx";
import NotFoundPage from "@/components/pages/NotFound.tsx";
import ProductsList from "@/components/test/product/ProductsList.tsx";
import Search from "@/components/pages/Search.tsx";

export const routes = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        children: [
            {
                index: true,
                element: <Home />,
            },
            {
                path: "subcategories/:id", //params
                element: <CategoriesList />,
            },
            {
                path: "categories",
                element: <CategoriesList />,
            },
            {
                path: "about",
                element: <About />,
            },
            {
                path:"products",
                element:<ProductsList/>
            },
            {
                path: "categories/:slug",
                element: <CategoryDetail />,
            },
            {
                path:"search",
                element:<Search />
            },
            {
                path: "*",
                element: <NotFoundPage />,
            },
        ],
    },
]);
//
// import {createBrowserRouter} from "react-router";
// import Layout from "@/components/Layout.tsx";
//
// export const routes = createBrowserRouter([
//     {
//         path: "/",
//         element:<Layout />,
//         children:[
//             {
//                 index:true,
//                 element:<div>Home page</div>
//             },
//             {
//                 path:'about',
//                 element:<div>About page</div>
//             },
//         ]
//     }
//
//
// ])