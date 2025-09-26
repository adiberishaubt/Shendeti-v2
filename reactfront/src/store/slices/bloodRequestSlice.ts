import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { BloodRequest } from '../../types';

interface BloodRequestState {
  bloodRequests: BloodRequest[];
  isLoading: boolean;
  error: string | null;
}

const initialState: BloodRequestState = {
  bloodRequests: [],
  isLoading: false,
  error: null,
};

export const fetchBloodRequests = createAsyncThunk(
  'bloodRequests/fetchBloodRequests',
  async (_, { rejectWithValue }) => {
    try {
      const bloodRequests = await api.getBloodRequests();
      return bloodRequests;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch blood requests');
    }
  }
);

export const createBloodRequest = createAsyncThunk(
  'bloodRequests/createBloodRequest',
  async (data: any, { rejectWithValue }) => {
    try {
      const bloodRequest = await api.createBloodRequest(data);
      return bloodRequest;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to create blood request');
    }
  }
);

const bloodRequestSlice = createSlice({
  name: 'bloodRequests',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchBloodRequests.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchBloodRequests.fulfilled, (state, action) => {
        state.isLoading = false;
        state.bloodRequests = action.payload;
      })
      .addCase(fetchBloodRequests.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      })
      .addCase(createBloodRequest.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(createBloodRequest.fulfilled, (state, action) => {
        state.isLoading = false;
        state.bloodRequests.push(action.payload);
      })
      .addCase(createBloodRequest.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = bloodRequestSlice.actions;
export default bloodRequestSlice.reducer;
