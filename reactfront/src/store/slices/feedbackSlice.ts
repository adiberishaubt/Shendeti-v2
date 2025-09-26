import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Feedback } from '../../types';

interface FeedbackState {
  feedback: Feedback[];
  isLoading: boolean;
  error: string | null;
}

const initialState: FeedbackState = {
  feedback: [],
  isLoading: false,
  error: null,
};

export const fetchFeedback = createAsyncThunk(
  'feedback/fetchFeedback',
  async (_, { rejectWithValue }) => {
    try {
      const feedback = await api.getFeedback();
      return feedback;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch feedback');
    }
  }
);

const feedbackSlice = createSlice({
  name: 'feedback',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchFeedback.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchFeedback.fulfilled, (state, action) => {
        state.isLoading = false;
        state.feedback = action.payload;
      })
      .addCase(fetchFeedback.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = feedbackSlice.actions;
export default feedbackSlice.reducer;
