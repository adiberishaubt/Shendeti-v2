import { configureStore } from "@reduxjs/toolkit";
import userReducer from "../slices/userSlice"
import countryReducer from "../slices/countrySlice"
import cityReducer from "../slices/citySlice"
import levelReducer from "../slices/levelSlice"
import specializationReducer from "../slices/specializationSlice"
import serviceReducer from "../slices/serviceSlice"
import DelModuleReducer from "../slices/delModuleSlice"

export const store = configureStore({
    reducer: {
        user: userReducer,
        country: countryReducer,
        deleteModule: DelModuleReducer,
        city: cityReducer,
        level: levelReducer,
        specialization: specializationReducer,
        service: serviceReducer
    }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch