const accessTokenKey = "accessToken";
const refreshTokenKey = "refreshToken";
const userKey = "authenticatedUser";

const elements = {
    authForms: document.getElementById("authForms"),
    dashboard: document.getElementById("dashboard"),
    welcomeMessage: document.getElementById("welcomeMessage"),
    logoutButton: document.getElementById("logoutButton"),
    dashboardTitle: document.getElementById("dashboardTitle"),
    dashboardEmail: document.getElementById("dashboardEmail"),
    dashboardRole: document.getElementById("dashboardRole"),
    refreshButton: document.getElementById("refreshButton"),
    refreshStatus: document.getElementById("refreshStatus"),
    registerForm: document.getElementById("registerForm"),
    loginForm: document.getElementById("loginForm"),
    registerStatus: document.getElementById("registerStatus"),
    loginStatus: document.getElementById("loginStatus")
};

function setStatus(element, message) {
    element.textContent = message;
}

function saveSession(data) {
    localStorage.setItem(accessTokenKey, data.accessToken);
    if (data.refreshToken) {
        localStorage.setItem(refreshTokenKey, data.refreshToken);
    }

    const user = {
        email: data.email,
        role: data.role
    };
    localStorage.setItem(userKey, JSON.stringify(user));
    renderAuthenticatedState(user);
}

function clearSession() {
    localStorage.removeItem(accessTokenKey);
    localStorage.removeItem(refreshTokenKey);
    localStorage.removeItem(userKey);
    renderGuestState();
}

function getStoredUser() {
    const raw = localStorage.getItem(userKey);
    if (!raw) return null;
    try {
        return JSON.parse(raw);
    } catch {
        return null;
    }
}

function renderAuthenticatedState(user) {
    elements.authForms.classList.add("hidden");
    elements.dashboard.classList.remove("hidden");
    elements.logoutButton.classList.remove("hidden");
    elements.welcomeMessage.textContent = `Welcome, ${user.email}`;
    elements.dashboardTitle.textContent = `Welcome, ${user.email}`;
    elements.dashboardEmail.textContent = `Email: ${user.email}`;
    elements.dashboardRole.textContent = `Role: ${user.role}`;
}

function renderGuestState() {
    elements.authForms.classList.remove("hidden");
    elements.dashboard.classList.add("hidden");
    elements.logoutButton.classList.add("hidden");
    elements.welcomeMessage.textContent = "Welcome, guest";
}

async function sendJson(url, body) {
    const response = await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify(body)
    });

    const text = await response.text();
    let data = null;
    if (text) {
        try { data = JSON.parse(text); } catch { data = { message: text }; }
    }

    if (!response.ok) {
        throw new Error(data?.message || data?.title || "Request failed.");
    }

    return data;
}

elements.registerForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    setStatus(elements.registerStatus, "");

    const email = document.getElementById("registerEmail").value.trim();
    const password = document.getElementById("registerPassword").value;
    const repeatPassword = document.getElementById("registerRepeatPassword").value;

    if (password !== repeatPassword) {
        setStatus(elements.registerStatus, "Passwords do not match.");
        return;
    }

    try {
        const data = await sendJson("/api/auth/register", {
            email,
            password,
            repeatPassword
        });

        saveSession(data);
        elements.registerForm.reset();
        setStatus(elements.registerStatus, "Registration successful.");
    } catch (error) {
        setStatus(elements.registerStatus, error.message);
    }
});

elements.loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();
    setStatus(elements.loginStatus, "");

    const email = document.getElementById("loginEmail").value.trim();
    const password = document.getElementById("loginPassword").value;

    try {
        const data = await sendJson("/api/auth/login", { email, password });
        saveSession(data);
        elements.loginForm.reset();
        setStatus(elements.loginStatus, "Login successful.");
    } catch (error) {
        setStatus(elements.loginStatus, error.message);
    }
});

elements.logoutButton.addEventListener("click", async () => {
    try {
        await fetch("/api/auth/logout", {
            method: "POST",
            credentials: "include"
        });
    } finally {
        clearSession();
    }
});

elements.refreshButton.addEventListener("click", async () => {
    setStatus(elements.refreshStatus, "Refreshing...");

    try {
        const data = await sendJson("/api/auth/refresh", {});
        saveSession(data);
        setStatus(elements.refreshStatus, "Access token refreshed.");
    } catch (error) {
        clearSession();
        setStatus(elements.refreshStatus, "Session expired. Please log in again.");
    }
});

const storedUser = getStoredUser();
if (storedUser && localStorage.getItem(accessTokenKey)) {
    renderAuthenticatedState(storedUser);
} else {
    renderGuestState();
}
