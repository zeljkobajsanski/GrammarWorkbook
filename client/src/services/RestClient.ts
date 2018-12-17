import axios from 'axios'

const baseUrl = 'data';

const restClient = {
    getExerciseUnit: (id: string | number) => {
        return axios.get(`${baseUrl}/units/${id}.json`);
    }
};

export default restClient;