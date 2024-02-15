export const patientsReducer = (state, action) => {
    switch(action.type) {
        case "loading":
            return {...state, loading: true}
        case "error":
            return {...state, loading: false, error: action.error}
        case "get-patient":
            return {...state, loading: false, error: null, patient: action.patient}
        case "all-patient":
            return {...state, loading: false, error: null, patients: action.patients}
        default:
            return state

    }
}