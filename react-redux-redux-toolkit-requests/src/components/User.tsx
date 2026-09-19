import { useDispatch } from "react-redux";
import "./User.css";
import {
  changeStatus,
  removeUser,
} from "../redux-toolkit/slices/userServerSlice";
import { AppDispatch } from "../redux-toolkit/store/store";
import { IUser } from "../redux-toolkit/interfaces/IUser";

interface UserProps {
  user: IUser;
}

function User({ user }: UserProps) {
  const dispatch = useDispatch<AppDispatch>();

  const handlerRemove = (id: string) => {
    dispatch(removeUser(id));
  };

  const handlerStatus = (currentUser: IUser) => {
    dispatch(changeStatus(currentUser));
  };

  return (
    <div>
      <p className={user.isActive ? "success" : "error"}>
        <button onClick={() => handlerStatus(user)}>
          {user.isActive ? "block" : "unblock"}
        </button>{" "}
        {user.name} {" "}
        <button onClick={() => handlerRemove(String(user.id))}>Del</button>
      </p>
    </div>
  );
}

export default User;
