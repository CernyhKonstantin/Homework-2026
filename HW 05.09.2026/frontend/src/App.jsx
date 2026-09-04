import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";

const API_URL = "https://localhost:7048/api/v1";
const AUTH_URL = "https://localhost:7048/api/Auth";

async function apiRequest(path, options = {}, token = "") {
  const headers = new Headers(options.headers || {});
  if (token) headers.set("Authorization", `Bearer ${token}`);
  const response = await fetch(`${API_URL}${path}`, { ...options, headers });
  const text = await response.text();
  let data = null;
  try { data = text ? JSON.parse(text) : null; } catch { data = text; }
  if (!response.ok) {
    const message = typeof data === "string" ? data : data?.message || JSON.stringify(data);
    throw new Error(message || `Request failed with status ${response.status}`);
  }
  return data;
}

function LoginForm({ onLogin }) {
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm();
  const [error, setError] = useState("");

  const submit = async (values) => {
    setError("");
    try {
      const response = await fetch(`${AUTH_URL}/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values)
      });
      const text = await response.text();
      const result = text ? JSON.parse(text) : null;
      if (!response.ok) throw new Error(result?.message || result || "Login failed");
      localStorage.setItem("accessToken", result.accessToken);
      localStorage.setItem("userEmail", result.email);
      localStorage.setItem("userRole", result.role);
      onLogin(result);
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <section className="card login-card">
      <h2>Admin / Moderator Login</h2>
      <p className="muted">Creation endpoints require the Admin or Moderator role.</p>
      <form onSubmit={handleSubmit(submit)}>
        <label>Email</label>
        <input type="email" {...register("email", { required: "Email is required" })} />
        {errors.email && <span className="error">{errors.email.message}</span>}
        <label>Password</label>
        <input type="password" {...register("password", { required: "Password is required" })} />
        {errors.password && <span className="error">{errors.password.message}</span>}
        <button disabled={isSubmitting}>{isSubmitting ? "Signing in..." : "Sign in"}</button>
        {error && <div className="alert error-box">{error}</div>}
      </form>
    </section>
  );
}

function CategoryForm({ token, onCreated }) {
  const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm();
  const [message, setMessage] = useState("");
  const submit = async (values) => {
    setMessage("");
    try {
      const data = await apiRequest("/categories", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          name: values.name,
          slug: values.slug,
          parentId: values.parentId ? Number(values.parentId) : null
        })
      }, token);
      reset();
      setMessage(`Category created successfully. ID: ${data.id}`);
      onCreated();
    } catch (err) { setMessage(`Error: ${err.message}`); }
  };
  return (
    <section className="card">
      <h2>Create Category</h2>
      <form onSubmit={handleSubmit(submit)}>
        <label>Name</label>
        <input placeholder="Electronics" {...register("name", { required: "Name is required", maxLength: 100 })} />
        {errors.name && <span className="error">{errors.name.message || "Maximum 100 characters"}</span>}
        <label>Slug</label>
        <input placeholder="electronics" {...register("slug", { required: "Slug is required", maxLength: 100 })} />
        {errors.slug && <span className="error">{errors.slug.message || "Maximum 100 characters"}</span>}
        <label>Parent category ID <span className="muted">(optional)</span></label>
        <input type="number" min="1" placeholder="Leave empty for root category" {...register("parentId", { min: 1 })} />
        <button disabled={isSubmitting}>{isSubmitting ? "Creating..." : "Create Category"}</button>
        {message && <div className="alert">{message}</div>}
      </form>
    </section>
  );
}

function ProductForm({ token, categories, onCreated }) {
  const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm({ defaultValues: { isActive: true } });
  const [message, setMessage] = useState("");
  const submit = async (values) => {
    setMessage("");
    try {
      const formData = new FormData();
      formData.append("Name", values.name);
      formData.append("Description", values.description || "");
      formData.append("Price", String(values.price));
      formData.append("StockQty", String(values.stockQty));
      formData.append("IsActive", String(Boolean(values.isActive)));
      formData.append("CategoryId", String(values.categoryId));
      Array.from(values.images || []).forEach(file => formData.append("Images", file));

      const data = await apiRequest("/products", { method: "POST", body: formData }, token);
      reset({ isActive: true });
      setMessage(`Product created successfully. ID: ${data.id}`);
      onCreated();
    } catch (err) { setMessage(`Error: ${err.message}`); }
  };
  return (
    <section className="card">
      <h2>Create Product</h2>
      {categories.length === 0 && <div className="alert">Create a category first.</div>}
      <form onSubmit={handleSubmit(submit)}>
        <label>Name</label>
        <input placeholder="Wireless Headphones" {...register("name", { required: "Name is required", maxLength: 200 })} />
        {errors.name && <span className="error">{errors.name.message}</span>}
        <label>Description</label>
        <textarea rows="4" placeholder="Product description" {...register("description", { maxLength: 2000 })} />
        {errors.description && <span className="error">Maximum 2000 characters</span>}
        <div className="grid-2">
          <div><label>Price</label><input type="number" step="0.01" min="0" {...register("price", { required: "Price is required", min: 0 })} />{errors.price && <span className="error">{errors.price.message}</span>}</div>
          <div><label>Stock quantity</label><input type="number" min="0" {...register("stockQty", { required: "Stock quantity is required", min: 0 })} />{errors.stockQty && <span className="error">{errors.stockQty.message}</span>}</div>
        </div>
        <label>Category</label>
        <select {...register("categoryId", { required: "Category is required" })}>
          <option value="">Select a category</option>
          {categories.map(category => <option key={category.id} value={category.id}>{category.name} (ID {category.id})</option>)}
        </select>
        {errors.categoryId && <span className="error">{errors.categoryId.message}</span>}
        <label>Images <span className="muted">(up to 5: JPG, JPEG, PNG, WEBP)</span></label>
        <input type="file" multiple accept=".jpg,.jpeg,.png,.webp" {...register("images", { validate: files => !files || files.length <= 5 || "Maximum 5 images" })} />
        {errors.images && <span className="error">{errors.images.message}</span>}
        <label className="checkbox"><input type="checkbox" {...register("isActive")} /> Active</label>
        <button disabled={isSubmitting || categories.length === 0}>{isSubmitting ? "Creating..." : "Create Product"}</button>
        {message && <div className="alert">{message}</div>}
      </form>
    </section>
  );
}

export default function App() {
  const [auth, setAuth] = useState(() => {
    const token = localStorage.getItem("accessToken");
    return token ? { accessToken: token, email: localStorage.getItem("userEmail"), role: localStorage.getItem("userRole") } : null;
  });
  const [categories, setCategories] = useState([]);
  const [products, setProducts] = useState([]);
  const [loadError, setLoadError] = useState("");

  const loadData = async () => {
    try {
      setLoadError("");
      const [categoryData, productData] = await Promise.all([
        apiRequest("/categories"),
        apiRequest("/products")
      ]);
      setCategories(categoryData || []);
      setProducts(productData || []);
    } catch (err) { setLoadError(err.message); }
  };

  useEffect(() => { loadData(); }, []);

  const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("userEmail");
    localStorage.removeItem("userRole");
    setAuth(null);
  };

  if (!auth) {
    return <main className="page"><header><h1>HW 05.09.2026</h1><p>Category & Product Creation</p></header><LoginForm onLogin={setAuth} /><div className="hint">Default seeded admin: <strong>admin@example.com</strong> / <strong>ChangeMe123!</strong></div></main>;
  }

  return (
    <main className="page">
      <header className="topbar"><div><h1>HW 05.09.2026</h1><p>React Hook Form + ASP.NET Core + SQL Server</p></div><div className="user"><span>{auth.email} · {auth.role}</span><button className="secondary" onClick={logout}>Logout</button></div></header>
      {auth.role !== "Admin" && auth.role !== "Moderator" && <div className="alert error-box">Your role is not allowed to create categories or products.</div>}
      <div className="forms">
        <CategoryForm token={auth.accessToken} onCreated={loadData} />
        <ProductForm token={auth.accessToken} categories={categories} onCreated={loadData} />
      </div>
      {loadError && <div className="alert error-box">Could not load data: {loadError}</div>}
      <section className="card data-card"><h2>Categories in SQL Server</h2>{categories.length ? <div className="table-wrap"><table><thead><tr><th>ID</th><th>Name</th><th>Slug</th><th>Parent ID</th></tr></thead><tbody>{categories.map(c => <tr key={c.id}><td>{c.id}</td><td>{c.name}</td><td>{c.slug}</td><td>{c.parentId ?? "—"}</td></tr>)}</tbody></table></div> : <p className="muted">No categories yet.</p>}</section>
      <section className="card data-card"><h2>Products in SQL Server</h2>{products.length ? <div className="table-wrap"><table><thead><tr><th>ID</th><th>Name</th><th>Price</th><th>Stock</th><th>Category</th><th>Active</th></tr></thead><tbody>{products.map(p => <tr key={p.id}><td>{p.id}</td><td>{p.name}</td><td>{Number(p.price).toFixed(2)}</td><td>{p.stockQty}</td><td>{p.categoryName}</td><td>{p.isActive ? "Yes" : "No"}</td></tr>)}</tbody></table></div> : <p className="muted">No products yet.</p>}</section>
    </main>
  );
}
