import {combineReducers, configureStore} from '@reduxjs/toolkit';
import worksheet from "../features/worksheet/worksheetSlice";

const reducer = combineReducers({
    worksheet
})

export const store = configureStore({
    reducer
});
