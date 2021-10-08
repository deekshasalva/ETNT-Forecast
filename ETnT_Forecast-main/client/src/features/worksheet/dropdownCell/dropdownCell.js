import React, {useState} from 'react';
import {Autocomplete, TextField} from "@mui/material";
import {setColValue} from "../worksheetSlice";
import {useDispatch} from "react-redux";
import _ from "lodash"
import './dropdownCell.css'

const DropdownCell = (props) => {
    const [inputValue, setInputValue] = useState('');
    const dispatch = useDispatch();

    function onChangeHandler(e, value) {
        dispatch(setColValue({id: props.data.id, key: props.colDef.field, value: value}))
    }

    function onInputChangeHandler(e, inputValue) {
        setInputValue(inputValue);
    }

    return (
        <Autocomplete
            options={props.options.map(x => _.defaultTo(x.value, x.fullName))}
            freeSolo
            value={props.value}
            onChange={onChangeHandler}
            inputValue={inputValue}
            onInputChange={onInputChangeHandler}
            disableClearable
            renderInput={(params) => (
                <TextField
                    {...params}
                    style={{padding: '5px 0'}}
                    placeholder={'Select ' + props.column.colId}
                    sx={{padding: 0}}
                />
            )}
            sx={{
                padding: 0
            }}
        />
    );
};

export default DropdownCell;