import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Specialization } from '../../types';

interface SpecializationState {
  specializations: Specialization[];
  isLoading: boolean;
  error: string | null;
}

const initialState: SpecializationState = {
  specializations: [],
  isLoading: false,
  error: null,
};

export const fetchSpecializations = createAsyncThunk(
  'specializations/fetchSpecializations',
  async (_, { rejectWithValue }) => {
    try {
      const specializations = await api.getSpecializations();
      return specializations;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch specializations');
    }
  }
);

const specializationSlice = createSlice({
  name: 'specializations',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchSpecializations.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchSpecializations.fulfilled, (state, action) => {
        state.isLoading = false;
        state.specializations = action.payload;
      })
      .addCase(fetchSpecializations.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = specializationSlice.actions;
export default specializationSlice.reducer;
