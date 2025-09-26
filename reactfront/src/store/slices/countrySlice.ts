import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Country } from '../../types';

interface CountryState {
  countries: Country[];
  isLoading: boolean;
  error: string | null;
}

const initialState: CountryState = {
  countries: [],
  isLoading: false,
  error: null,
};

export const fetchCountries = createAsyncThunk(
  'countries/fetchCountries',
  async (_, { rejectWithValue }) => {
    try {
      const countries = await api.getCountries();
      return countries;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch countries');
    }
  }
);

const countrySlice = createSlice({
  name: 'countries',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCountries.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchCountries.fulfilled, (state, action) => {
        state.isLoading = false;
        state.countries = action.payload;
      })
      .addCase(fetchCountries.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = countrySlice.actions;
export default countrySlice.reducer;
