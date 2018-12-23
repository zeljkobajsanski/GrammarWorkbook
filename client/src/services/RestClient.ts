import axios from 'axios'

const baseUrl = '/api';

const restClient = {
    getExerciseUnit: (id: string | number) => {
        return axios.get(`${baseUrl}/units/${id}`);
    },
    saveTopic(topic: {}) {
        return axios.post(`${baseUrl}/topics`, topic);
    },
    getTopic(id: string) {
        return axios.get(`${baseUrl}/topics/${id}`);
    },
    saveExercise(exercise: {}) {
        return axios.post(`${baseUrl}/exercises`, exercise);
    },
    removeExercise(id: string) {
        return axios.delete(`${baseUrl}/exercises/${id}`);
    },
    checkResults(unit: {}) {
        return axios.post(`${baseUrl}/units/CheckResults`, unit);
    }
};

export default restClient;