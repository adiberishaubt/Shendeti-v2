import { createSlice } from "@reduxjs/toolkit";

interface DelModuleState {
    id: number,
    opened: boolean,
    handleAction: ((id: number) => void) | null;
}

const initialState: DelModuleState = {
  id: 0,
  opened: false,
  handleAction : null
};

const DelModuleSlice = createSlice({
  name: "DeleteModule",
  initialState,
  reducers: {
    toggleModal: (state, action) => {
      state.opened = !state.opened;
      state.id = action.payload.id;
      state.handleAction = action.payload.handleAction;
    }
  }
});

export const { toggleModal } = DelModuleSlice.actions;

export default DelModuleSlice.reducer;
