import Footer from '../Footer';

export default function Layout({ children }) {
	return (
		<div className="main">
			<h1>Downstat.us</h1>

			{children}

			<Footer />
		</div>
	);
}