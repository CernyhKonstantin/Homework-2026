import { useEffect, useState } from "react";
import Modal from "./components/Modal";
import "./App.css";

const USERS_KEY = "portal_demo_users";
const SESSION_KEY = "portal_demo_session";

function readUsers() {
  try {
    return JSON.parse(localStorage.getItem(USERS_KEY) || "[]");
  } catch {
    return [];
  }
}

function App() {
  const [modal, setModal] = useState(null);
  const [users, setUsers] = useState(readUsers);
  const [session, setSession] = useState(() => {
    try {
      return JSON.parse(localStorage.getItem(SESSION_KEY) || "null");
    } catch {
      return null;
    }
  });
  const [message, setMessage] = useState("");

  useEffect(() => {
    localStorage.setItem(USERS_KEY, JSON.stringify(users));
  }, [users]);

  const openModal = (type) => {
    setMessage("");
    setModal(type);
  };

  const closeModal = () => setModal(null);

  const register = (email, password, repeatPassword) => {
    const normalizedEmail = email.trim().toLowerCase();

    if (!normalizedEmail || !password || !repeatPassword) {
      setMessage("Please fill in all fields.");
      return;
    }

    if (password.length < 6) {
      setMessage("Password must contain at least 6 characters.");
      return;
    }

    if (password !== repeatPassword) {
      setMessage("Passwords do not match.");
      return;
    }

    if (users.some((user) => user.email === normalizedEmail)) {
      setMessage("An account with this email already exists.");
      return;
    }

    const user = { email: normalizedEmail, password };
    const nextUsers = [...users, user];
    setUsers(nextUsers);
    localStorage.setItem(USERS_KEY, JSON.stringify(nextUsers));

    const newSession = { email: normalizedEmail, accessToken: createToken() };
    localStorage.setItem(SESSION_KEY, JSON.stringify(newSession));
    localStorage.setItem("accessToken", newSession.accessToken);
    setSession(newSession);
    closeModal();
  };

  const login = (email, password) => {
    const normalizedEmail = email.trim().toLowerCase();
    const user = users.find((item) => item.email === normalizedEmail && item.password === password);

    if (!user) {
      setMessage("Invalid email or password.");
      return;
    }

    const newSession = { email: user.email, accessToken: createToken() };
    localStorage.setItem(SESSION_KEY, JSON.stringify(newSession));
    localStorage.setItem("accessToken", newSession.accessToken);
    setSession(newSession);
    closeModal();
  };

  const logout = () => {
    localStorage.removeItem(SESSION_KEY);
    localStorage.removeItem("accessToken");
    setSession(null);
    setMessage("You have been logged out.");
  };

  return (
    <main className="app">
      <header className="header">
        <div>
          <span className="brand">Portal Auth</span>
          <span className="subtitle">React Portal Authentication</span>
        </div>
        <nav className="actions">
          {session ? (
            <>
              <span className="welcome">Welcome, {session.email}</span>
              <button className="button button--secondary" onClick={logout}>Logout</button>
            </>
          ) : (
            <>
              <button className="button button--secondary" onClick={() => openModal("login")}>Login</button>
              <button className="button" onClick={() => openModal("register")}>Register</button>
            </>
          )}
        </nav>
      </header>

      <section className="hero">
        <div className="hero-card">
          <span className="eyebrow">Homework: Portal authentication</span>
          <h1>Register and authenticate users through React portals.</h1>
          <p>
            The authentication forms are rendered into the dedicated <code>#modal</code> DOM node
            using ReactDOM <code>createPortal</code>.
          </p>
          <div className="hero-actions">
            {!session && <button className="button" onClick={() => openModal("register")}>Create account</button>}
            {!session && <button className="button button--secondary" onClick={() => openModal("login")}>Sign in</button>}
            {session && <div className="signed-in">Authenticated as <strong>{session.email}</strong></div>}
          </div>
          {message && <p className="page-message">{message}</p>}
        </div>
      </section>

      <Modal open={modal === "register"} closeModal={closeModal} title="Create account">
        <AuthForm type="register" onSubmit={register} switchForm={() => { setMessage(""); setModal("login"); }} message={message} />
      </Modal>

      <Modal open={modal === "login"} closeModal={closeModal} title="Sign in">
        <AuthForm type="login" onSubmit={login} switchForm={() => { setMessage(""); setModal("register"); }} message={message} />
      </Modal>
    </main>
  );
}

function AuthForm({ type, onSubmit, switchForm, message }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    onSubmit(email, password, repeatPassword);
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <label htmlFor={`${type}-email`}>Email</label>
      <input id={`${type}-email`} type="email" value={email} onChange={(event) => setEmail(event.target.value)} autoComplete="email" required />

      <label htmlFor={`${type}-password`}>Password</label>
      <input id={`${type}-password`} type="password" value={password} onChange={(event) => setPassword(event.target.value)} autoComplete={type === "login" ? "current-password" : "new-password"} minLength={6} required />

      {type === "register" && (
        <>
          <label htmlFor="register-repeat-password">Repeat password</label>
          <input id="register-repeat-password" type="password" value={repeatPassword} onChange={(event) => setRepeatPassword(event.target.value)} autoComplete="new-password" minLength={6} required />
        </>
      )}

      {message && <div className="form-message" role="alert">{message}</div>}

      <button className="button button--full" type="submit">
        {type === "register" ? "Register" : "Login"}
      </button>

      <p className="switch-text">
        {type === "register" ? "Already have an account?" : "Do not have an account?"}{" "}
        <button type="button" className="link-button" onClick={switchForm}>
          {type === "register" ? "Login" : "Register"}
        </button>
      </p>
    </form>
  );
}

function createToken() {
  if (window.crypto?.randomUUID) return window.crypto.randomUUID();
  return `${Date.now()}-${Math.random().toString(36).slice(2)}`;
}

export default App;
