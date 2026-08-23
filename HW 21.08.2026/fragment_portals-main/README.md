# Portal Authentication Homework

This project demonstrates user registration and authentication through React portals.

## What is implemented

- Registration form with email, password, and repeat password.
- Login form with email and password.
- Both forms are rendered inside a reusable `Modal` component.
- `Modal` uses `createPortal` to render into the dedicated `#modal` DOM node.
- Registration validates required fields, password length, matching passwords, and duplicate email addresses.
- Successful registration creates a local demo session.
- Successful login creates a local demo session.
- The session and access token are stored in `localStorage`.
- Logout removes the session and access token.
- The page displays `Welcome, email` after authentication.
- Escape closes the modal and clicking the backdrop closes it.

## Important

This is a frontend homework/demo implementation. There is no backend in the supplied starter project, so authentication is simulated with `localStorage` rather than a real server. In a production application, passwords must never be stored in `localStorage`; registration and login should call a backend API and only the server should validate credentials.

## Run

```bash
npm install
npm run dev
```

Build for production:

```bash
npm run build
```

## Portal structure

The HTML contains two root nodes:

```html
<div id="root"></div>
<div id="modal"></div>
```

The normal application is rendered into `#root`, while authentication dialogs are rendered into `#modal` using `createPortal`.
