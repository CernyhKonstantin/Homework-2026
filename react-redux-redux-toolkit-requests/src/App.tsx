import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import reactLogo from "./assets/react.svg";
import reduxLogo from "/redux.svg";
import "./App.css";
import UserForm from "./components/UserForm";
import User from "./components/User";
import { getUsers } from "./redux-toolkit/slices/userServerSlice";
import { AppDispatch, RootState } from "./redux-toolkit/store/store";

function App() {
  const dispatch = useDispatch<AppDispatch>();
  const { users, loading, error } = useSelector(
    (state: RootState) => state.userServer
  );

  useEffect(() => {
    dispatch(getUsers());
  }, [dispatch]);

  return (
    <>
      <div>
        <a href="https://redux.js.org/" target="_blank" rel="noreferrer">
          <img src={reduxLogo} className="logo" alt="Redux logo" />
        </a>
        <a href="https://react.dev" target="_blank" rel="noreferrer">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>

      <h3>React + Redux Toolkit + JSON Server</h3>

      <div className="card">
        <UserForm />
      </div>

      <div className="card">
        {loading && <p>Loading...</p>}
        {error && <p className="error">{error}</p>}

        {!loading && !users.length && <h3>No users</h3>}

        {users.map((user) => (
          <User user={user} key={user.id} />
        ))}
      </div>
    </>
  );
}

export default App;
