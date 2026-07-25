import { NavLink } from "react-router";

const Header = () => {

    const menu = [
        { title: "Home", path: "/" },
        { title: "Categories", path: "/categories" },
        { title: "Products", path: "/products" },
        { title: "About", path: "/about" },
        { title: "Contacts", path: "/contacts" }
    ];

    return (
        <header className="bg-slate-900 text-white shadow-lg">

            <div className="mx-auto flex h-16 max-w-7xl items-center justify-between px-6">

                <NavLink
                    to="/"
                    className="text-2xl font-bold tracking-wide"
                >
                    MyShop
                </NavLink>

                <nav className="flex gap-6">

                    {menu.map(item => (

                        <NavLink
                            key={item.path}
                            to={item.path}
                            className={({ isActive }) =>
                                `transition hover:text-blue-400 ${
                                    isActive
                                        ? "text-blue-400"
                                        : "text-white"
                                }`
                            }
                        >
                            {item.title}
                        </NavLink>

                    ))}

                </nav>

                <div className="flex items-center gap-5 text-xl">

                    <button>🔍</button>

                    <button>❤️</button>

                    <button className="relative">

                        🛒

                        <span
                            className="absolute -right-2 -top-2 flex h-5 w-5 items-center justify-center rounded-full bg-red-500 text-xs"
                        >
                            3
                        </span>

                    </button>

                    <button>👤</button>

                </div>

            </div>

        </header>
    );
};

export default Header;