import View from './view';

import useSearch from '../../hooks/useSearch';
import useStatusHomepage from '../../hooks/useStatusHomepage';

export default function Home(props) {
	return (<View {...props} useSearch={useSearch} useStatusHomepage={useStatusHomepage} />);
}