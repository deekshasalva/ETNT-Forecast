import {createAsyncThunk, createSlice} from "@reduxjs/toolkit";
import _ from "lodash";
import api from "../../utils/api";
import {FORECAST, LOOKUP, TASK, UPLOAD} from "../../utils/endpoints";
import {parse} from "json2csv";
import {v4} from 'uuid';

let cancelToken;
const INITIAL_VALUE = {
    forecast: [],
    selectedYear: new Date().getFullYear(),
    orgs: [],
    users: [],
    projects: [],
    skills: [],
    business: [],
    capabilities: [],
    forecastConfidence: [],
    fyYears: [],
    app: {
        saving: false,
        uploading: false,
        selectedRow: ""
    }
}

const EMPTY_FORECAST = {
    id: "",
    org: "",
    manager: "",
    usFocal: "",
    project: "",
    skillGroup: "",
    business: "",
    capability: "",
    chargeline: "",
    forecastConfidence: "",
    comments: "",
    jan: "",
    feb: "",
    mar: "",
    apr: "",
    may: "",
    june: "",
    july: "",
    aug: "",
    oct: "",
    sep: "",
    nov: "",
    dec: ""
}

export const getForecast = createAsyncThunk(
    "forecast/get",
    async (selectedFyYear) => {
        const response = await api.get(FORECAST + "/" + selectedFyYear);
        return response.data.result;
    }
)

export const getLookupData = createAsyncThunk(
    "forecast/lookup",
    async () => {
        const response = await api.get(LOOKUP);
        return response.data.result;
    }
)

export const uploadForecast = createAsyncThunk("forecast/upload",
    async (file, thunkAPI) => {
        const formData = new FormData();
        formData.append("file", file);
        const response = await api.post(UPLOAD, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        thunkAPI.dispatch(getTaskStatus(response.data.result));
        return response.data.result;
    })

export const saveForecast = createAsyncThunk("forecast/save",
    async (_, thunkAPI) => {
        const response = await api.post(
            FORECAST,
            JSON.stringify(
                {
                    forecasts: thunkAPI.getState().worksheet.forecast
                        .filter(x => x.isEdited)
                        .map(x => ({...x, year: thunkAPI.getState().worksheet.selectedYear.toString()}))
                }));
        thunkAPI.dispatch(setAppState({key: 'saving', value: false}));
        return response.data.result;
    })

export const deleteForecast = createAsyncThunk("forecast/delete", async (_, thunkAPI) => {
    const year = thunkAPI.getState().worksheet.selectedYear;
    const forecastId = thunkAPI.getState().worksheet.app.selectedRow;
    const response = await api.delete(FORECAST + `/${forecastId}/year/${year}`);
    thunkAPI.dispatch(getForecast(year))
})

export const getTaskStatus = createAsyncThunk("forecast/taskStatus",
    async (taskId, thunkAPI) => {
        let response = await api.get(TASK + taskId);
        while (!response.data.result || !['Completed', 'Failed'].includes(response.data.result.status)) {
            await new Promise(resolve => setTimeout(resolve, 3000));
            response = await api.get(TASK + taskId)
        }
        thunkAPI.dispatch(setAppState({key: 'uploading', value: false}));
        thunkAPI.dispatch(getForecast(thunkAPI.getState().worksheet.selectedYear))
        if (response.data.result.status == 'Failed') {
            const data = JSON.parse(response.data.result.errors);
            const fields = ['Errors', 'Id', 'Org', 'Manager', 'USFocal', 'Project', 'SkillGroup', 'Business', 'Capability', 'Chargeline', 'ForecastConfidence', 'Comments', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', 'Year']
            const csv = parse(data, fields);
            console.log(csv);
            const url = window.URL
                .createObjectURL(new Blob([csv]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'error.csv');
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    }
)

export const worksheetSlice = createSlice({
    name: "forecast",
    initialState: INITIAL_VALUE,
    reducers: {
        setColValue: (state, action) => {
            const rowId = action.payload.id;
            const forecast = state.forecast.filter(x => x.id == rowId)[0];
            if (forecast) {
                forecast[action.payload.key] = action.payload.value;
                forecast["isEdited"] = true;
            }
        },
        setSelectedYear: (state, action) => {
            state.selectedYear = action.payload;
        },
        setAppState: (state, action) => {
            state.app[action.payload.key] = action.payload.value;
        },
        addRow: (state,action)=>{
            state.forecast.push({...EMPTY_FORECAST,id:v4()});
        }
    },
    extraReducers: {
        [getForecast.fulfilled]: (state, action) => {
            state.forecast = action.payload;
        },
        [getLookupData.fulfilled]: (state, action) => {
            state.orgs = action.payload.orgs;
            state.users = action.payload.users;
            state.projects = action.payload.projects;
            state.skills = action.payload.skills;
            state.business = action.payload.business;
            state.capabilities = action.payload.capabilities;
            state.forecastConfidence = action.payload.forecastConfidence;
            state.fyYears = action.payload.fyYears;
            const currentYearIndex = state.fyYears.indexOf(new Date().getFullYear())
            state.selectedYear = currentYearIndex > -1 ? state.fyYears[currentYearIndex] : _.last(state.fyYears)
        }
    }
});

export const {setColValue, setSelectedYear, setAppState,addRow} = worksheetSlice.actions;
export const selectForecast = (state) => state.worksheet.forecast;
export const selectFyYears = (state) => state.worksheet.fyYears;
export const selectSelectedYear = (state) => state.worksheet.selectedYear;
export const selectOrgs = (state) => state.worksheet.orgs;
export const selectUsers = (state) => state.worksheet.users;
export const selectProjects = (state) => state.worksheet.projects;
export const selectSkills = (state) => state.worksheet.skills;
export const selectBusiness = (state) => state.worksheet.business;
export const selectCapability = (state) => state.worksheet.capabilities;
export const selectCategory = (state) => state.worksheet.forecastConfidence;
export const selectAppState = (state) => state.worksheet.app;
export default worksheetSlice.reducer;