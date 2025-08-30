import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { Country } from "../types/country";

export const getAllCountries = createAsyncThunk('all/countries', async (_, thunkAPI) => {
    try{
        const response = await axios.get('http://localhost:5095/Country/list');
        return response.data;
    }
    catch(error) {
        return thunkAPI.rejectWithValue('something went wrong');
    }
})

interface countryState {
    countries: Country[],
    isLoading: boolean
}

const initialState : countryState = {  
    countries: [],
    isLoading: true
}

const CountrySlice = createSlice({
    name: "countries",
    initialState,
    reducers: {
    },
    extraReducers: (builder) => {
        builder
          .addCase(getAllCountries.pending, (state) => {
            state.isLoading = true;
          })
          .addCase(getAllCountries.fulfilled, (state, action) => {
            state.countries = action.payload;
            state.isLoading = false;
          })
          .addCase(getAllCountries.rejected, (state) => {
            state.isLoading = false;
          });
    },
})

export default CountrySlice.reducer;