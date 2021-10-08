import React, {useEffect, useRef, useState} from 'react';
import './App.css';
import Worksheet from "./features/worksheet/worksheet";
import {useDispatch, useSelector} from "react-redux";
import {
    getForecast,
    getLookupData,
    saveForecast,
    selectAppState,
    selectFyYears,
    selectSelectedYear,
    setAppState,
    setSelectedYear,
    uploadForecast
} from "./features/worksheet/worksheetSlice";
import {Dropdown} from "semantic-ui-react";


const saveButton = () => {

}

function App() {
    const worksheetRef = useRef();
    const dispatch = useDispatch();
    const {saving, uploading} = useSelector(selectAppState);
    const fyYears = useSelector(selectFyYears);
    const selectedYear = useSelector(selectSelectedYear)
    const [isDirty, setIsDirty] = useState(false);

    const onFileChange = event => {
        if (event.target.files[0]) {
            dispatch(setAppState({key: 'uploading', value: true}))
            dispatch(uploadForecast(event.target.files[0]));
        }
    };

    const handleItemClick = (_e, x) => {
        dispatch(setSelectedYear(x.value));
    };

    const handleSaveClick = () => {
        if(!saving) {
            dispatch(setAppState({key: 'saving', value: true}));
            dispatch(saveForecast());
        }
    }

    useEffect(() => {
        dispatch(getLookupData())
    }, [])

    useEffect(() => {
        if (selectedYear) {
            dispatch(getForecast(selectedYear));
        }
    }, [selectedYear])

    // useEffect(()=>{
    //     console.log(isDirty);
    //     if(isDirty){
    //         dispatch(saveForecast());
    //     }
    // },[isDirty])

    return (
        <div className="App">
            <div className="ui top fixed inverted menu">
                <div className="ui container no-left-margin">
                    <a className="header strong item" href="#">
                        ET & T
                    </a>
                </div>
                <Dropdown
                    className="ui item link"
                    name="Year"
                    placeholder="Select Year"
                    pointing
                    value={selectedYear}
                    text={selectedYear}
                >
                    <Dropdown.Menu>
                        {
                            fyYears.map(year => (
                                <Dropdown.Item
                                    value={year}
                                    text={year}
                                    onClick={handleItemClick}
                                />
                            ))
                        }
                    </Dropdown.Menu>
                </Dropdown>
                <div className={`ui item ${saving ? "" : "clickable"}`} onClick={handleSaveClick}>
                    {saving
                        ? (
                            <div className="ui active dimmer">
                                <div className="ui loader small"></div>
                            </div>
                        )
                        : (
                            <i className="save icon large"/>
                        )

                    }
                </div>
                <div className="ui item clickable">
                    <i className="download icon large no-left-margin" onClick={() => {
                        worksheetRef.current.onBtExport()
                    }}></i>
                </div>
                <div className={`ui item ${uploading ? "" : "clickable"}`}
                     onClick={() => !uploading && document.getElementById("upload").click()}>
                    {uploading
                        ? (
                            <div className="ui active dimmer">
                                <div className="ui loader small"></div>
                            </div>
                        )
                        : (
                            <i className="upload icon large"/>
                        )

                    }

                    <input id="upload" type="file" onChange={onFileChange} hidden/>
                </div>
            </div>
            <div className="ui main text container">
                <Worksheet ref={worksheetRef} setIsDirty={setIsDirty}/>
            </div>
        </div>
    );
}

export default App;
