import { FC } from "react"
import styles from "./UserSavedPage.module.scss";
import SavedCard from "../../components/saved-card/SavedCard";

const UserSavedPage: FC = function () {
    return (
        <div className={`w-full bg-white ${styles["user-saved-page"]}`}>
            <div>
                <h3 className="text-3xl font-bold mb-1">
                    Збережені
                </h3>
                <p className="text-base text-gray-500 dark:text-gray-300">
                    Тут ви можете переглянути всі збережені вами заклади
                </p>
            </div>
            <div className="w-full flex flex-row gap-2 rounded-xl p-3 bg-primary-100">
                <p className="font-medium">Збережених закладів:</p>
                <p className="font-bold text-primary-700">3</p>
            </div>
            <div className={styles["cards-cont"]}>
                <SavedCard title="Піца парк" imgUrl="/images/test-rest-img.png"
                    placeLink="/details/2" />
                <SavedCard title="Familia Grande" imgUrl="https://assets.dots.live/misteram-public/1606a7ce-cf02-46c4-a097-7fe6759bde43.png"
                    placeLink="/details/1" />
                <SavedCard title="Дім 18" imgUrl="https://localhost:7088/Images/managers/VuHVmq659uW8jIwdZNYLWXalaly2/202311131819018435.png"
                    placeLink="/details/47" />
            </div>
        </div>
    )
}

export default UserSavedPage;