import packageJson from '../../package.json';
const HOST = 'https://localhost:7012/api';
export const enviroments = {
  context: 'develop',
  API_PUBLIC: HOST + '/',
  version: packageJson.version,
};
