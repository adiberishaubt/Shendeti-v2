import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "../configurations/axiosConfig";
import { PaginatedItems } from "../types/paginated";
import { Service } from "../types/service";

export const getAllServices = createAsyncThunk('all/services', async (_, thunkAPI) => {
    try{
        const response = await axios.get('http://localhost:5095/Service/list');
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

export const getPaginatedServices = createAsyncThunk('all/services/getPaginated', async (pageIndex: number = 1, thunkAPI) => {
    try{
        const response = await axios.get(`http://localhost:5095/Service?pageIndex=${pageIndex}`);
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

interface serviceState {
    services: Service[],
    servicesPaginated: PaginatedItems<Service> | null,
    isLoading: boolean
}

const initialState : serviceState = {  
    services: [],
    servicesPaginated: null,
    isLoading: true
}

const ServiceSlice = createSlice({
    name: "service",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
          .addCase(getAllServices.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getAllServices.fulfilled, (state, action) => {
            state.isLoading = false;
            state.services = action.payload;
          })
          .addCase(getAllServices.rejected, (state) => {
            state.isLoading = false;
          })
          .addCase(getPaginatedServices.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getPaginatedServices.fulfilled, (state, action) => {
            state.servicesPaginated = action.payload;
            state.isLoading = false;
          })
          .addCase(getPaginatedServices.rejected, (state) => {
            state.isLoading = false;
          });
    }
})

export default ServiceSlice.reducer;