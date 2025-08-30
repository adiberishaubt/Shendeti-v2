import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { Level } from "../types/level";
import { PaginatedItems } from "../types/paginated";

export const getPaginatedLevels = createAsyncThunk('all/levels/paginated', async (pageIndex: number = 1, thunkAPI) => {
    try{
        const response = await axios.get(`http://localhost:5095/Level?pageIndex=${pageIndex}`);
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

interface levelState {
    levels: PaginatedItems<Level> | null,
    isLoading: boolean
}

const initialState : levelState = {  
    levels: null,
    isLoading: true
}

const LevelSlice = createSlice({
    name: "levels",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
          .addCase(getPaginatedLevels.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getPaginatedLevels.fulfilled, (state, action) => {
            state.levels = action.payload;
            state.isLoading = false;
          })
          .addCase(getPaginatedLevels.rejected, (state) => {
            state.isLoading = false;
          });
    },
})

export default LevelSlice.reducer;