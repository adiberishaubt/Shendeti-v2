import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { PaginatedItems } from "../types/paginated";
import { Specialization } from "../types/specialization";

export const getAllSpecializations = createAsyncThunk('all/specializations', async (_, thunkAPI) => {
    try{
        const response = await axios.get('http://localhost:5095/Specialization/list');
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

export const getPaginatedSpecializations = createAsyncThunk('all/specializations/getPaginated', async (pageIndex: number = 1, thunkAPI) => {
    try{
        const response = await axios.get(`http://localhost:5095/Specialization?pageIndex=${pageIndex}`);
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

interface specializationState {
    specializations: Specialization[],
    specializationsPaginated: PaginatedItems<Specialization> | null,
    isLoading: boolean
}

const initialState : specializationState = {  
    specializations: [],
    specializationsPaginated: null,
    isLoading: true
}

const SpecializationSlice = createSlice({
    name: "specialization",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
          .addCase(getAllSpecializations.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getAllSpecializations.fulfilled, (state, action) => {
            state.isLoading = false;
            state.specializations = action.payload;
          })
          .addCase(getAllSpecializations.rejected, (state) => {
            state.isLoading = false;
          })
          .addCase(getPaginatedSpecializations.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getPaginatedSpecializations.fulfilled, (state, action) => {
            state.specializationsPaginated = action.payload;
            state.isLoading = false;
          })
          .addCase(getPaginatedSpecializations.rejected, (state) => {
            state.isLoading = false;
          });
    },
})

export default SpecializationSlice.reducer;