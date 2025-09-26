import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Slot } from '../../types';

interface SlotState {
  slots: Slot[];
  isLoading: boolean;
  error: string | null;
}

const initialState: SlotState = {
  slots: [],
  isLoading: false,
  error: null,
};

export const fetchSlots = createAsyncThunk(
  'slots/fetchSlots',
  async (_, { rejectWithValue }) => {
    try {
      const slots = await api.getSlots();
      return slots;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch slots');
    }
  }
);

const slotSlice = createSlice({
  name: 'slots',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchSlots.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchSlots.fulfilled, (state, action) => {
        state.isLoading = false;
        state.slots = action.payload;
      })
      .addCase(fetchSlots.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = slotSlice.actions;
export default slotSlice.reducer;
