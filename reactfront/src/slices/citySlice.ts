import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { City } from "../types/city"
import axios from "../configurations/axiosConfig";
import { PaginatedItems } from "../types/paginated";

export const getAllCities = createAsyncThunk('all/cities', async (_, thunkAPI) => {
    try{
        const response = await axios.get('http://localhost:5095/City/list');
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

export const getPaginatedCities = createAsyncThunk('all/cities/getPaginated', async (pageIndex: number = 1, thunkAPI) => {
    try{
        const response = await axios.get(`http://localhost:5095/City?pageIndex=${pageIndex}`);
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

interface cityState {
    cities: City[],
    citiesPaginated: PaginatedItems<City> | null,
    isLoading: boolean
}

const initialState : cityState = {  
    cities: [],
    citiesPaginated: null,
    isLoading: true
}

const CitySlice = createSlice({
    name: "cities",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
          .addCase(getAllCities.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getAllCities.fulfilled, (state, action) => {
            state.isLoading = false;
            state.cities = action.payload;
          })
          .addCase(getAllCities.rejected, (state) => {
            state.isLoading = false;
          })
          .addCase(getPaginatedCities.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getPaginatedCities.fulfilled, (state, action) => {
            state.citiesPaginated = action.payload;
            state.isLoading = false;
          })
          .addCase(getPaginatedCities.rejected, (state) => {
            state.isLoading = false;
          });
    },
})

export default CitySlice.reducer;