import Axios from "axios";

const httpAxios = Axios.create({
  baseURL: process.env.REACT_APP_LIKE_API_URL,
  headers: {
    Accept: "application/json",
  },
});

const LikeApi = class {
  static get(resource) {
    return httpAxios.get(resource);
  }

  static post(resource, data) {
    return httpAxios.post(resource, data);
  }
};

export default LikeApi;
