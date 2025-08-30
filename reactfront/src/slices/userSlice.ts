import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axiosInstance from "../configurations/axiosConfig"
import LoginCredentials from "../types/login"
import { AxiosError } from "axios"
import { User } from "../types/user"
import Cookie from "js-cookie"

interface UserState {
    user: User | null,
    isLoggedIn: boolean,
    isLoading: boolean,
    error: string
}

export const login = createAsyncThunk('users/login', async (creds: LoginCredentials, { rejectWithValue }) => {
        try {
            const response = await axiosInstance.post(`User/login`, creds);
            return await response.data;
        } catch (error: any) {
            return rejectWithValue((error as AxiosError<any>).response?.data);
        }
    }
)

export const getMyUser = createAsyncThunk('users/me', async (_, { rejectWithValue }) => {
        try {
            const response = await axiosInstance.get(`User/me`);
            return await response.data;
        } catch (error: any) {
            return rejectWithValue((error as AxiosError<any>).response?.data);
        }
    }
)

const initialState: UserState = {
    user: null,
    isLoggedIn: false,
    isLoading: true,
    error: ''
}

export const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setFalseLoading: (state) => {
            state.isLoading = false;
        },
        resetError: (state) => {
            state.error = '';
        },
        logout: (state) => {
            state.isLoggedIn = false;
            state.isLoading = false;
            state.user = null;
            Cookie.remove('jwt');
            Cookie.remove('refreshToken');
        }
    },
    extraReducers: builder => {
        builder.addCase(login.pending, (state) => {
            state.isLoading = true;
        }),
        builder.addCase(login.fulfilled, (state) => {
            state.isLoading = false;
            state.isLoggedIn = true;
        }),
        builder.addCase(login.rejected, (state, action) => {
            state.isLoading = false;
            state.error = action.payload as string;
            return;
        }),
        builder.addCase(getMyUser.pending, (state) => {
            state.isLoading = true;
        }),
        builder.addCase(getMyUser.fulfilled, (state, action) => {
            state.isLoading = false;
            state.isLoggedIn = true;
            state.user = action.payload;
        }),
        builder.addCase(getMyUser.rejected, (state) => {
            state.isLoading = false;
        })
    }
})

export const { resetError, logout, setFalseLoading } = userSlice.actions;
export default userSlice.reducer