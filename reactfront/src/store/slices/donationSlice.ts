import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Donation } from '../../types';

interface DonationState {
  donations: Donation[];
  isLoading: boolean;
  error: string | null;
}

const initialState: DonationState = {
  donations: [],
  isLoading: false,
  error: null,
};

export const fetchDonations = createAsyncThunk(
  'donations/fetchDonations',
  async (_, { rejectWithValue }) => {
    try {
      const donations = await api.getDonations();
      return donations;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch donations');
    }
  }
);

export const createDonation = createAsyncThunk(
  'donations/createDonation',
  async (data: any, { rejectWithValue }) => {
    try {
      const donation = await api.createDonation(data);
      return donation;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to create donation');
    }
  }
);

const donationSlice = createSlice({
  name: 'donations',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchDonations.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchDonations.fulfilled, (state, action) => {
        state.isLoading = false;
        state.donations = action.payload;
      })
      .addCase(fetchDonations.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      })
      .addCase(createDonation.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(createDonation.fulfilled, (state, action) => {
        state.isLoading = false;
        state.donations.push(action.payload);
      })
      .addCase(createDonation.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = donationSlice.actions;
export default donationSlice.reducer;
