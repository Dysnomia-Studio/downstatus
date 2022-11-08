import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';

import Home from './components/Home';

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<Routes>
				<Route key="/" index={true} element={<Home />} />
			</Routes>
		);
	}
}
