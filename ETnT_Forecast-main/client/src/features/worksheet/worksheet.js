import React, {forwardRef, useEffect, useImperativeHandle, useState} from 'react';
import {AgGridReact} from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';
import './worksheet.css';
import {useDispatch, useSelector} from "react-redux";
import {
    addRow,
    deleteForecast,
    selectBusiness,
    selectCapability,
    selectCategory,
    selectForecast,
    selectOrgs,
    selectProjects,
    selectSkills,
    selectUsers,
    setAppState,
    setColValue
} from "./worksheetSlice";
import DropdownCell from "./dropdownCell/dropdownCell";

const Worksheet = forwardRef((props, ref) => {
    const forecast = useSelector(selectForecast);
    const dispatch = useDispatch();
    const [gridApi, setGridApi] = useState(null);
    const [gridColumnApi, setGridColumnApi] = useState(null);
    const [rowData, setRowData] = useState(null);

    useImperativeHandle(
        ref,
        () => ({
            onBtExport() {
                gridApi.exportDataAsCsv();
            }
        }),
    )
    useEffect(() => {
        setRowData(forecast);
        // gridApi?.sizeColumnsToFit();
        autoSizeAll();
    }, [forecast]);

    const autoSizeAll = (skipHeader) => {
        var allColumnIds = [];
        gridColumnApi?.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });
        gridColumnApi?.autoSizeColumns(allColumnIds);
    };

    const onGridReady = (params) => {
        setGridApi(params.api);
        setGridColumnApi(params.columnApi);
    };

    const onBtExport = () => {
        gridApi.exportDataAsExcel();
    };
    const valueSetters = params => {
        dispatch(setColValue({id: params.data.id, key: params.colDef.field, value: params.newValue}))
    };

    const columnDef = [
        {
            headerName: "ET&T Org",
            field: 'org',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectOrgs)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "Manager",
            field: 'manager',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectUsers)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "US Focal",
            field: 'usFocal',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectUsers)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "Project",
            field: 'project',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectProjects)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "Skill Group",
            field: 'skillGroup',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectSkills)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "Business Unit",
            field: 'business',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectBusiness)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {
            headerName: "Capabilities",
            field: 'capability',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectCapability)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {headerName: "Chargeline", field: 'chargeLine', cellStyle: {padding: '0'}},
        {
            headerName: "Forecast Confidence",
            field: 'forecastConfidence',
            cellRenderer: 'dropDownRenderer',
            cellRendererParams: {options: useSelector(selectCategory)},
            editable: false,
            cellStyle: {padding: '0'}
        },
        {headerName: "Comments", field: 'comments'},
        {headerName: "Jan", field: 'jan'},
        {headerName: "Feb", field: 'feb'},
        {headerName: "Mar", field: 'mar'},
        {headerName: "Apr", field: 'apr'},
        {headerName: "May", field: 'may'},
        {headerName: "June", field: 'june'},
        {headerName: "July", field: 'july'},
        {headerName: "Aug", field: 'aug'},
        {headerName: "Sept", field: 'sep'},
        {headerName: "Oct", field: 'oct'},
        {headerName: "Nov", field: 'nov'},
        {headerName: "Dec", field: 'dec'},
    ]

    const onCellEditingStopped = (e) => {
        props.setIsDirty(true);
    }

    // Delete row on delete key press
    const onSelectionChanged = () => {
        const id = gridApi.getSelectedRows()[0].id;
        dispatch(setAppState({key: "selectedRow", value: id}))
    };

    const deleteRow = (event) => {
        if (event.key == 'Backspace' && event.shiftKey) {
            dispatch(deleteForecast())
        }
        if(event.key=='Enter' && event.shiftKey){
            dispatch(addRow());
        }
    };

    useEffect(() => {
        document.addEventListener("keydown", deleteRow);

        return () => {
            document.removeEventListener("keydown", deleteRow);
        };
    }, []);
    const defaultColumnDef = {resizeable: true, sortable: true, filter: true, editable: true, valueSetter: valueSetters}
    return (
        <div className="container">
            <div className="ag-theme-alpine fullwidth-grid">
                <AgGridReact
                    onFirstDataRendered={autoSizeAll}
                    onGridReady={onGridReady}
                    columnDefs={columnDef}
                    rowData={rowData}
                    defaultColDef={defaultColumnDef}
                    onCellEditingStopped={onCellEditingStopped}
                    frameworkComponents={{dropDownRenderer: DropdownCell}}
                    rowSelection={'single'}
                    onSelectionChanged={onSelectionChanged}
                />
            </div>
        </div>
    );
});

export default Worksheet;