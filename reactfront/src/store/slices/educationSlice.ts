import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Education } from '../../types';

interface EducationState {
  educations: Education[];
  isLoading: boolean;
  error: string | null;
}

const initialState: EducationState = {
  educations: [],
  isLoading: false,
  error: null,
};

export const fetchEducations = createAsyncThunk(
  'educations/fetchEducations',
  async (_, { rejectWithValue }) => {
    try {
      const educations = await api.getEducations();
      return educations;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch educations');
    }
  }
);

const educationSlice = createSlice({
  name: 'educations',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchEducations.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchEducations.fulfilled, (state, action) => {
        state.isLoading = false;
        state.educations = action.payload;
      })
      .addCase(fetchEducations.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = educationSlice.actions;
export default educationSlice.reducer;
