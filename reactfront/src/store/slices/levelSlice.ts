import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import api from '../../services/api';
import { Level } from '../../types';

interface LevelState {
  levels: Level[];
  isLoading: boolean;
  error: string | null;
}

const initialState: LevelState = {
  levels: [],
  isLoading: false,
  error: null,
};

export const fetchLevels = createAsyncThunk(
  'levels/fetchLevels',
  async (_, { rejectWithValue }) => {
    try {
      const levels = await api.getLevels();
      return levels;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Failed to fetch levels');
    }
  }
);

const levelSlice = createSlice({
  name: 'levels',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchLevels.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(fetchLevels.fulfilled, (state, action) => {
        state.isLoading = false;
        state.levels = action.payload;
      })
      .addCase(fetchLevels.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.payload as string;
      });
  },
});

export const { clearError } = levelSlice.actions;
export default levelSlice.reducer;
