import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import axios from "axios";
import { IUser } from "../interfaces/IUser";

const API_URL = "http://localhost:3000/users";

interface IUserState {
  users: IUser[];
  loading: boolean;
  error: string | null;
}

const initialState: IUserState = {
  users: [],
  loading: false,
  error: null,
};

export const getUsers = createAsyncThunk<IUser[], void, { rejectValue: string }>(
  "users/getUsers",
  async (_, { rejectWithValue }) => {
    try {
      const response = await axios.get<IUser[]>(API_URL);
      return response.data;
    } catch {
      return rejectWithValue("Could not load users from the server.");
    }
  }
);

export const addUser = createAsyncThunk<
  IUser,
  Omit<IUser, "id">,
  { rejectValue: string }
>("users/addUser", async (user, { rejectWithValue }) => {
  try {
    const response = await axios.post<IUser>(API_URL, user);
    return response.data;
  } catch {
    return rejectWithValue("Could not add the user to the server.");
  }
});

export const removeUser = createAsyncThunk<
  string,
  string,
  { rejectValue: string }
>("users/removeUser", async (id, { rejectWithValue }) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
    return id;
  } catch {
    return rejectWithValue("Could not delete the user from the server.");
  }
});

export const changeStatus = createAsyncThunk<
  IUser,
  IUser,
  { rejectValue: string }
>("users/changeStatus", async (user, { rejectWithValue }) => {
  try {
    const response = await axios.patch<IUser>(`${API_URL}/${user.id}`, {
      isActive: !user.isActive,
    });
    return response.data;
  } catch {
    return rejectWithValue("Could not change the user status.");
  }
});

export const userServerSlice = createSlice({
  name: "users",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getUsers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(getUsers.fulfilled, (state, action: PayloadAction<IUser[]>) => {
        state.loading = false;
        state.users = action.payload;
      })
      .addCase(getUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Could not load users.";
      })
      .addCase(addUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addUser.fulfilled, (state, action: PayloadAction<IUser>) => {
        state.loading = false;
        state.users.push(action.payload);
      })
      .addCase(addUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Could not add the user.";
      })
      .addCase(removeUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(removeUser.fulfilled, (state, action: PayloadAction<string>) => {
        state.loading = false;
        state.users = state.users.filter((user) => String(user.id) !== action.payload);
      })
      .addCase(removeUser.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Could not delete the user.";
      })
      .addCase(changeStatus.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(changeStatus.fulfilled, (state, action: PayloadAction<IUser>) => {
        state.loading = false;
        const index = state.users.findIndex(
          (user) => String(user.id) === String(action.payload.id)
        );
        if (index !== -1) {
          state.users[index] = action.payload;
        }
      })
      .addCase(changeStatus.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload ?? "Could not change the user status.";
      });
  },
});

export default userServerSlice.reducer;
