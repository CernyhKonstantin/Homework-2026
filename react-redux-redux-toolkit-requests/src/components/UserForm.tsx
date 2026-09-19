import { FormEvent, useState } from "react";
import { useDispatch } from "react-redux";
import { AppDispatch } from "../redux-toolkit/store/store";
import { addUser } from "../redux-toolkit/slices/userServerSlice";

function UserForm() {
  const dispatch = useDispatch<AppDispatch>();
  const [name, setName] = useState("");

  const handlerAddUser = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const trimmedName = name.trim();
    if (!trimmedName) {
      return;
    }

    await dispatch(
      addUser({
        name: trimmedName,
        isActive: true,
      })
    );

    setName("");
  };

  return (
    <form onSubmit={handlerAddUser}>
      <label>
        Name: {" "}
        <input
          type="text"
          value={name}
          onChange={(event) => setName(event.target.value)}
          placeholder="Enter a user name"
        />{" "}
        <button type="submit">Save</button>
      </label>
    </form>
  );
}

export default UserForm;
