# React Redux Toolkit Requests

This project implements the requested JSON Server integration.

## Completed tasks

1. Data from `db.json` is loaded from the JSON Server API and displayed in the React interface.
2. The old `userSlice` was removed.
3. User creation is handled by `UserForm.tsx` and sent directly to JSON Server with a POST request.
4. User delete and status changes are also persisted on the server.
5. Redux Toolkit now uses only `userServerSlice` for user data.
6. `json-server` is included as a project dependency.

## Start the project

Install dependencies:

```bash
npm install
```

Start JSON Server in terminal 1:

```bash
npx json-server db.json
```

or use the configured npm script:

```bash
npm run server
```

The API is available at:

```text
http://localhost:3000/users
```

Start the React/Vite application in terminal 2:

```bash
npm run dev
```

The frontend reads users from the server when the application starts.

## User creation flow

`UserForm.tsx` dispatches the `addUser` async thunk. The thunk sends:

```http
POST http://localhost:3000/users
```

The returned server record is then added to the Redux state.

## Project structure

```text
src/
├── components/
│   ├── User.tsx
│   ├── User.css
│   └── UserForm.tsx
├── redux-toolkit/
│   ├── interfaces/
│   │   └── IUser.ts
│   ├── slices/
│   │   └── userServerSlice.ts
│   └── store/
│       └── store.ts
├── App.tsx
└── main.tsx
```
